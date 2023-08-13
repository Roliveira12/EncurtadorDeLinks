.PHONY: infra-up infra-down api-build api-up api-down start stop

infra-up:
	docker-compose up -d redis postgres rabbitmq

infra-down:
	docker-compose down

api-build:
	docker-compose build api

api-up:
	docker-compose up -d api

api-down:
	docker-compose down api

start: infra-up api-up

stop: api-down infra-down
