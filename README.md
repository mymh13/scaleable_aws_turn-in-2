This is just a study project to try to combine Docker Swarm with a simple .NET MVC application
  
The goal is not to build a fullscale application, or even set things up properly, the aim for this is simply to explore and try to get a grasp of the mechanics within the systems.  

Feel free to browse around but do not expect this to be a functional or safe setup for anything you might need!
  
---
  
The "howto-scalable-form.pdf" has a huge tutorial describing how to build this project.

---
  
Daily cycles: startup
  
```bash
# load in .env variables
source .env

# check your session IP (if you run VPN or change locations)
curl -s https://checkip.amazonaws.com
# set that new IP in dev.params.json in AllowedSshCidr

# create the stack
aws cloudformation create-stack \
  --region "$REGION" \
  --stack-name "$STACK" \
  --template-url "https://s3.${REGION}.amazonaws.com/${BUCKET}/${PREFIX}root.yaml" \
  --parameters file://"$PARAMS" \
  --capabilities CAPABILITY_IAM CAPABILITY_NAMED_IAM

aws cloudformation wait stack-create-complete \
  --region "$REGION" --stack-name "$STACK"
```
  
Daily cycles: update the stack
  
```bash
aws cloudformation update-stack \
  --region "$REGION" \
  --stack-name "$STACK" \
  --template-url "https://s3.${REGION}.amazonaws.com/${BUCKET}/${PREFIX}root.yaml" \
  --parameters file://"$PARAMS" \
  --capabilities CAPABILITY_IAM CAPABILITY_NAMED_IAM
```
  
  
Daily cycles: teardown
  
```bash
aws cloudformation delete-stack \
  --region "$REGION" \
  --stack-name "$STACK"

aws cloudformation wait stack-delete-complete \
  --region "$REGION" \
  --stack-name "$STACK"
```
  
---
  
This below is just a bash-section I keep to copy/paste code into the Word document that builds the .pdf. It is just a graphical helper. :)

```bash
aws s3 cp infra/artifacts/docker-stack.yml "s3://$BUCKET/${PREFIX}artifacts/docker-stack.yml" --region "$REGION"
```