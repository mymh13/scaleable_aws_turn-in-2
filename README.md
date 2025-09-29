This is the turn-in project for Skalbara Molnapplikationer for the CLO24-Program at Campus Mölndal. Author is Niklas Häll, 2025.
  
This project is building on this repo: https://github.com/mymh13/swarm-dotnet-test
It has been heavily modified and expanded upon from that foundation.

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
aws cloudformation describe-stacks \
  --region eu-west-1 \
  --stack-name swarm-cf-root \
  --query "Stacks[0].Outputs[?OutputKey=='GitHubEcrPushRoleArn'].OutputValue" \
  --output text
```
  