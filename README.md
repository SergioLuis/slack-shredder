# Slack Shredder
A CLI tool to quickly delete your uploaded Slack files

## Why?
I won't judge the reasons that led to your situation, but the cold, hard truth is that your Slack team has run out of space. To add drama to the problem, the Slack GUI only allows you to painfully delete files one by one. Cry no more! You can quickly delete all of your uploaded files with this tool.

## How?
With the Slack REST API. Is that simple. You only need your legacy authentication token, which can be obtained here: https://api.slack.com/custom-integrations/legacy-tokens

Then, you can just execute:

```
> SlackShredder.exe <slack token>
```

Keep in mind that, if you are on GNU/Linux or macOS, you need the Mono Runtime (no NET Core for this bad boy)

```
$ mono SlackShredder.exe <slack token>
```

## License
Do whatever you want with this project.