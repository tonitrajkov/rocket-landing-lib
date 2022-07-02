# Rocket Landing App

The solution is implemented in Microsoft Visual Studio 2019 using .Net Core 3.1 as a framework.
This solution is consisted of four libraries 
- Zartis.RocketLanding  - Library for the main business logic
- Zartis.RocketLanding.Abstractions
- Zartis.RocketLanding.Tests - Unit test implemented using xUnit.
- Zartis.RocketLanding.ConsoleTest - A console app for testing purposes 

## How to run

- Install Microsoft Visual Studio 2019 or above and install .Net Core 3.1 framework
- Open Zartis.RocketLanding.sln with the Visual Studio
- Build the solution to install and restore Nuget packages.
- Set project Zartis.RocketLanding.ConsoleTest as startup project and run it with Debug -> Start

## Library improvements

- Better configuration for setup the platform size.