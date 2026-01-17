# CtrlShell

# CtrlShell.SshTest  
Goal: prove that an agentless SSH connection can be created from a .NET app.

# One-time setup
> Run these commands only once.

dotnet new console -n CtrlShell.SshTest
cd CtrlShell.SshTest
dotnet add package SSH.NET

# Run
dotnet run
