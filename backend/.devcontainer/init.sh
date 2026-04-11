#!/usr/bin/env bash
set -e

dpkgArchitecture="amd64"
if [[ "$(uname -m)" ==  "arm64" ]]; then
	dpkgArchitecture="arm64"
fi

# Rosetta support
if [[ "$dpkgArchitecture" == "arm64" ]]; then
	sudo dpkg --add-architecture amd64
	sudo apt-get update
	sudo apt-get install binutils:amd64
fi

### Docker Multiarch
docker buildx create --name multiarch --driver docker-container --use

### Gum (used by go-task/cli for prompts)
echo 'deb [trusted=yes] https://repo.charm.sh/apt/ /' | sudo tee /etc/apt/sources.list.d/charm.list
sudo apt-get update
sudo apt install gum

### Google Cloud CLI
# sudo apt-get install apt-transport-https ca-certificates gnupg curl
# curl https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo gpg --dearmor -o /usr/share/keyrings/cloud.google.gpg
# echo "deb [signed-by=/usr/share/keyrings/cloud.google.gpg] https://packages.cloud.google.com/apt cloud-sdk main" | sudo tee -a /etc/apt/sources.list.d/google-cloud-sdk.list
# sudo apt-get update && sudo apt-get install google-cloud-cli

### Task
npm install -g @go-task/cli
### Api Spec Converter
npm install -g api-spec-converter
### TypeSpec && Dependencies
npm install -g @typespec/compiler
npm install

### Azure Artifacts Credential Provider
sudo wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash

# .NET Tools
dotnet tool install --global dotnet-outdated-tool
dotnet tool install --global docfx
dotnet tool install --global dotnet-ef --version 9.0.11

sudo chown -R vscode:vscode /home/vscode/.local
