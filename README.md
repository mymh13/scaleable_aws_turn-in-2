This is just a brief study project to try to combine Docker Swarm with a super basic .NET MVC application
  
The goal is not to build a fullscale application, or even set things up properly, the aim for this is simply to explore and try to get a grasp of the mechanics within the systems.  

Feel free to browse around but do not expect this to be a functional or safe setup for anything you might need!
  
---
  
The "howto-scalable-form.pdf" has a huge tutorial describing how to build this project.
  
---

```bash
/infra
  /templates
    root.yaml                  # orchestrates children
    00-sg-swarm.yaml           # Security Group for swarm
    10-ec2-swarm.yaml          # EC2 Manager + 2 Workers (Docker installed)
  /parameters
    dev.json                   # example parameter set for root.yaml
```
