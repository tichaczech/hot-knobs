#!/usr/bin/env bash
set -e

### Azure Artifacts Credential Provider
sudo wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash

### Docker Multiarch
docker buildx create --name multiarch --driver docker-container --use

### TypeSpec && Dependencies
npm install -g @typespec/compiler
npm install

### Api Spec Converter
npm install -g api-spec-converter

### .NET Tools
dotnet tool restore
