#!/usr/bin/env bash
set -e

dpkgArchitecture="amd64"

if [[ "$(uname -m)" ==  "arm64" ]]; then
	dpkgArchitecture="arm64"
fi

sudo apt-get update

# Rosetta support
if [[ "$dpkgArchitecture" == "arm64" ]]; then
	sudo dpkg --add-architecture amd64
	sudo apt-get update
	sudo apt-get install binutils:amd64
fi

### Docker Multiarch
docker buildx create --name multiarch --driver docker-container --use

### TypeSpec
npm install -g @typespec/compiler
### Task
npm install -g @go-task/cli
### Api Spec Converter
npm install -g api-spec-converter

### Ping
sudo apt-get --assume-yes install inetutils-ping

# .NET Tools
dotnet tool install --global dotnet-outdated-tool
dotnet tool install --global docfx
dotnet tool install --global dotnet-ef
