# Calendar App BDD Automation POC

A proof-of-concept web UI automation framework built with **C#**, **SpecFlow**, **NUnit**, and **Selenium WebDriver** using a **BDD approach**.

## Overview

This repository demonstrates a lightweight but structured BDD automation framework for a Calendar web application. The project validates:

- SpecFlow BDD integration
- Selenium WebDriver setup
- NUnit execution
- configuration-driven automation
- screenshot capture on failure
- Extent HTML report generation
- Azure DevOps pipeline integration

## Tech Stack

- C#
- .NET 8
- SpecFlow
- NUnit
- Selenium WebDriver
- Chrome browser
- Extent Reports
- Azure DevOps Pipelines

## Project Structure

```text
Framework-CalendarApp-POC
|-- Uhm.Framework.CalendarApp.Poc.Tests
|   |-- Drivers
|   |-- Features
|   |-- Hooks
|   |-- Pages
|   |-- StepDefinitions
|   |-- Support
|   |-- Utilities
|   |-- appsettings.json
|   |-- specflow.json
|   `-- Uhm.Framework.CalendarApp.Poc.Tests.csproj
|-- NuGet.Poc.Config
|-- azure-pipelines.yml
`-- README.md