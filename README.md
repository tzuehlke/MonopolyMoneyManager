# Intro
*MonopolyMoneyManager* is a Blazor WebAssembly to be used during Monopoly to manage Money transfers.

![Screenshot](https://user-images.githubusercontent.com/32843441/103153706-bad12c80-4792-11eb-880b-1bf966d6d2a1.png  "Screenshot")

# Documentation
...

# Build, Deploy
```
dotnet dev-certs https --trust
dotnet watch run
dotnet publish -c Release
```

# Plan for next releases
- [ ] History of transfers
- [x] Save Status as cookie
- [x] Create configuration page for setting players and amounts
- [x] Generate link with configuration/status for browser favorites
- [ ] make some automatic tests