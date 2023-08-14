.PHONY: infra-up infra-down api-build api-up api-down start stop

infra-up:
	docker-compose up -d redis postgres rabbitmq

infra-down:
	docker-compose down

local-build:
	dotnet build ShortenerUrl.sln

local-clean:
	dotnet clean ShortenerUrl.sln

local-run:
	dotnet watch --project src/WebApi/WebApi.csproj run	

api-up:
	docker-compose up -d api

api-down:
	docker-compose down api

start: infra-up api-up

stop: api-down infra-down
