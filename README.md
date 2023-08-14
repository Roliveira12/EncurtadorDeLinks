# URL Shortener

This project provides a .NET-based URL shortening service with integration to PostgreSQL for data storage and RabbitMQ for message queuing, utilizing the EasyNetQ framework. The project follows the Clean Architecture pattern.

## Features

- Shortening long URLs to short URLs.
- Storing data in PostgreSQL for tracking and analysis.
- Asynchronously sending shortened URLs to a RabbitMQ queue.

## Technologies Used

- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL](https://www.postgresql.org/)
- [RabbitMQ](https://www.rabbitmq.com/) with [EasyNetQ Framework](https://github.com/EasyNetQ/EasyNetQ)


## Architeture Used
- This project uses Clean Architeture in order to organize layers 

## Prerequisites

Ensure you have the following components installed:
- MakeFile ( You can proceed without this, but is cool )
- [.NET 6 SDK](https://dotnet.microsoft.com/download) - for building and running the project.
- [Docker](https://www.docker.com/get-started) - for running PostgreSQL and RabbitMQ containers and download images

If you don't have Docker installed you can use the WSL2 with an Linux Distribution to run docker-compose

## Setup

1. Clone this repository to your system:
    ```
   git clone https://github.com/Roliveira12/EncurtadorDeLinks.git
    ```

2. Navigate to the project directory:

   ```
   cd EncurtadorDeLinks
   ```

3. Build the project using the .NET SDK:

   ```bash
   dotnet build
   ```

## Usage

### 1. Running Application

In order to run the application you can use the MakeFile in root folder:

 If you want debug the application just run

```
  make infra-up 
```
This will run the docker-compose and initiates all the infra needed

### 1.1 Running Containerized
If you want to run it on a containered project you just have to run the command 

`make` start

This will start everthing you need to run application

The application is exposed on port `7027`

### Makefile

The project includes a Makefile to automate common tasks. Use the following commands:

- `make api up`: Run the project.
- `make infra-up`: Run Infrastructure.
- `make infra-down`: Down Infrastructure
- `make start`: Run entire container
- `make stop`: Stop everything

### 5. Shortening URLs

Use an HTTP client (e.g., cURL or Postman) to make a POST request to `http://localhost:7027/shorten` (or domain `http://shorten-url/shorten`) with JSON in the request body:

```json
{
    "url": "https://www.example.com"
}
```

### 6. Accessing ShortenedUrls

- First of all we need to create the URL

    Since you have started application just send a post to localhost:7027/shorten

    ```json
    {
        "url": "https://www.example.com"
    }
    ```

    If this is a valid url will return the output with 201(Created)

    Since they are created will send a event to RabbitMQ
    In fact they will send on Exchange: `shorted.url` the event `on.shorted.url.created`

    ```json
    {
      "url": "string",
      "shortUrl": "string",
      "id": "string",
    }
    ```
    Otherwise will return a bad request result if the detailed error

- Since you have created the url just pick the shortUrl parameter and pass this to 

   `localhost:7027/{shortUrlId}`

    If they exists, will redirect you to OriginalUrl and in background will increase one hit to the url on database 


## Contributing

Contributions are welcome! Feel free to open a pull request explaining your changes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

