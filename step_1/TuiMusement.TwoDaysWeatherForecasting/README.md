<br />

# TUI Musement Two Days Weather Forecasting interview project

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built with</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li>
          <a href="#how-to-build-and-run-the-cli-application">How to build and run the CLI application</a>
          <ul>
            <li><a href="#with-net-sdk">With .NET SDK</a></li>
            <li><a href="#with-vs-code-and-development-containers">With VS Code and Development Containers</a></li>
            <li><a href="#with-docker">With Docker</a></li>
          </ul>
        </li>
      </ul>
    </li>
    <li>
      <a href="#configuration">Configuration</a>
      <ul>
        <li><a href="#provide-configuration-setting-through-environment-variables">Provide configuration setting through environment variables</a></li>
        <li><a href="#provide-configuration-setting-through-command-line-arguments">Provide configuration setting through command line arguments</a></li>
      </ul>
    </li>
    <li><a href="#code-style">Code style</a></li>
    <li><a href="#to-do">To do</a></li>
    <li><a href="#known-issues">Known issues</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## About The Project

TUI Musement Two Days Weather Forecasting interview project is a CLI application that retrieve the weather forecasting for the cities returned by [TUI Musement Cities API](https://partner-api.musement.com/api/partner/catalog/cities/) leveraging [Weather Api Forecasting API](https://www.weatherapi.com/docs/#apis-forecast).

The architecture reflect my take on Clean Architecture even though it's far from comprehensive and it continue to evolve with every project.\
Some concepts/patterns of Domain Driven Design are also used in the design of the domain layer.\
It is is overkill for a project of this size, but the request was to
>show us your best

\
In the solution there are two project:
- [TuiMusement.TwoDaysWeatherForecasting.ConsoleApp](TuiMusement.TwoDaysWeatherForecasting.ConsoleApp)\
It contains the implementation of the CLI application.
- [TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests](TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests)\
It contains the unit tests.


### Built with

* [.NET 6](https://dotnet.microsoft.com/en-us/)
* [Automapper](https://automapper.org/)
* [Serilog](https://serilog.net/)
* [xUnit](https://xunit.net/)
* [FakeItEasy](https://fakeiteasy.github.io/)
* [AutoFixture](https://github.com/AutoFixture/AutoFixture)
* [Editorconfig](https://editorconfig.org/)

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

To build the project you'll need either the [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download), an IDE with [Development Containers](https://containers.dev/) support (as far as I know, only [VS Code](https://code.visualstudio.com/) support them at the moment) or [Docker](https://www.docker.com/).

### Prerequisites

* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download), [VS Code](https://docs.docker.com/get-docker/) or [Docker](https://docs.docker.com/get-started/)
* The source code
   ```sh
   git clone https://github.com/nluisetto/TuiMusementInterviewTest.git
   ```

<p align="right">(<a href="#top">back to top</a>)</p>

### How to build and run the CLI application

#### With .NET SDK

1. Enter the repo folder
   ```sh
   cd TuiMusementInterviewTest
   ```
2. Enter the solution folder
   ```sh
   cd step_1/TuiMusement.TwoDaysWeatherForecasting
   ```
3. Restore project dependencies
   ```sh
   dotnet restore "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.csproj"
   ```
4. Publish the project
   ```sh
   dotnet publish "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.csproj" -c Release -o ./your_preferred_output_path
   ```
5. Run (see [Configuration](#configuration) section for instructions on how to specify configuration settings)
   ```sh
   ./your_preferred_output_path/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp 
   ```

<p align="right">(<a href="#top">back to top</a>)</p>


#### With VS Code and Development Containers

Follow the instructions in [this page](https://code.visualstudio.com/docs/remote/containers) to know how to use VS Code & Development Containers.

<p align="right">(<a href="#top">back to top</a>)</p>


#### With Docker

1. Enter the repo folder
   ```sh
   cd TuiMusementInterviewTest
   ```
2. Run the script to build the docker image
   ```sh
   ./build-docker-image.sh
   ```
3. Run (see [Configuration](#configuration) section for instructions on how to specify configuration settings)
   ```sh
   docker run --rm nico/two-days-weather-forecasting
   ```

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONFIGURATION -->
## Configuration

TuiMusement.TwoDaysWeatherForecasting.ConsoleApp relies on three settings to work:
- the TUI Musement API base URL
- the WeatherApi API base URL
- the WeatherAPI API key

### Provide configuration setting through environment variables

Define an environment variable with name matching the one for the configuration setting you want to provide and the value you want the application to use.
- TUI Musement API base URL: `TuiApi__BaseUrl`
- WeatherApi API base URL: `WeatherApi__BaseUrl`
- WeatherAPI API key: `WeatherApi__ApiKey`

#### Example with .NET SDK
```shell
TuiApi__BaseUrl='https://api.musement.com' WeatherApi__BaseUrl='https://api.weatherapi.com' WeatherApi__ApiKey='YOUR_API_KEY' dotnet run
```

#### Example with Docker
```shell
docker run --rm -e TuiApi__BaseUrl='https://api.musement.com' -e WeatherApi__BaseUrl='https://api.weatherapi.com' -e WeatherApi__ApiKey='YOUR_API_KEY' nico/two-days-weather-forecasting
```

<p align="right">(<a href="#top">back to top</a>)</p>


### Provide configuration setting through command line arguments

Pass arguments with name matching the one for the configuration settings you want to provide and the value you want the application to use.
- TUI Musement API base URL: `TuiApi:BaseUrl`
- WeatherApi API base URL: `WeatherApi:BaseUrl`
- WeatherAPI API key: `WeatherApi:ApiKey`

#### Example with .NET SDK
```shell
./your_preferred_output_path/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp TuiApi:BaseUrl='https://api.musement.com' WeatherApi:BaseUrl='https://api.weatherapi.com' WeatherApi:ApiKey='YOUR_API_KEY'
```

#### Example with Docker
```shell
docker run --rm nico/two-days-weather-forecasting TuiApi:BaseUrl='https://api.musement.com' WeatherApi:BaseUrl='https://api.weatherapi.com' WeatherApi:ApiKey='66fb869bd7a44ea092f213503221207
```

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CODE STYLE -->
## Code style

Coding style must adhere to the rules specified in the [.editorconfig](./.editorconfig) file.\
Make sure that your IDE support [EditorConfig](https://editorconfig.org/) specs and that the supoprt is enabled.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- TO DO -->
## To do

- Write a description of the architecture
- Add default value for TUI Musement API base URL configuration setting
- Add default value for WeatherApi API base URL configuration setting

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- KNOWN ISSUES -->
## Known issues

Currently there are no known issues.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Thank you, but there is no need to contribute to this project.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.

<p align="right">(<a href="#top">back to top</a>)</p>