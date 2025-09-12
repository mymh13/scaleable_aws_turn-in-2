This is just a brief study project to try to combine Docker Swarm with a super basic .NET MVC application
  
The goal is not to build a fullscale application, or even set things up properly, the aim for this is simply to explore and try to get a grasp of the mechanics within the systems.  

Feel free to browse around but do not expect this to be a functional or safe setup for anything you might need!
  
---
  
The "howto-scalable-form.pdf" has a huge tutorial describing how to build this project.
  
---

```bash
aws cloudformation delete-stack \
  --region "$REGION" \
  --stack-name "$STACK"

aws cloudformation wait stack-delete-complete \
  --region "$REGION" --stack-name "$STACK"
```
