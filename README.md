# sample-api

## Development

dapr run --dapr-http-port 3500 --dapr-grpc-port 50001 --app-port 5250 --app-id api1

## Deployment

```
  sample-api:
    image: zlzforever/sample-api
    restart: always
    ports:
      - 9037:80
      - 9038:3500
  sample-api-dapr:
    image: 'daprio/daprd:1.10.7'
    command: ['./daprd', '-app-id', 'sample-api', '-app-port', '80', '-components-path', '/.dapr/components', '-config', '/.dapr/config.yaml']
    volumes:
      - './dapr:/.dapr'
    network_mode: 'service:sample-api'
    depends_on:
      - sample-api
```
