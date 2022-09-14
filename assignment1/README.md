# .NET containerization with Docker
## `1.0` Description
The local bakery (the customer) wants to have a presence on the web. They have provided the SW4BED Developer Team (you) with a codebase for which they need help to deploy to their cloud infrastructure.

The customer needs help writing the Dockerfiles needed to create Docker images for the two solutions: `bakery-api.sln` and `bakery-web.sln` and a Docker Compose file that can help them distribute the application internally.

## `1.1` Dockerfiles
Create Dockerfiles to build images for the two solutions.

1. Write a Dockerfile[^2] named `Dockerfile.API` that creates an image for `bakery-api.sln`
2. Write a Dockerfile named `Dockerfile.WEB` that creates and image for `bakery-web.sln`

_Hint: Place the Dockerfiles in the same directory as the solution files_

## `1.2` Docker Compose
1. Write a Docker Compose file that uses the two images with the following configuration:
    
    - Two services: A service named `api` for `bakery-api` and a service named `web` for `bakery-web`
    - Publish `bakery-api` on port `6000` on the host
    - Publish `bakery-web` on port `5000` on the host
    - `bakery-web` has an environment variable[^3] named `API_URL` that should contain an URL to the base address of the API service[^4]

## `1.3` Push images to Docker Hub
Publish your images to Docker Hub[^1]. 

1. Push the image built with `Dockerfile.API` in `1.1` to Docker Hub
2. Push the image built with `Dockerfile.WEB` in `1.1` to Docker Hub
3. If needed, update the images used in `docker-compose.yaml` in `1.2` so they match your published images.

## Submission
Before submitting your solution, do the following:
1. Create a folder named after your group name, eg. `assignment-1 2` and add the following artifacts:
    - `Dockerfile.API`
    - `Dockerfile.WEB`
    - `docker-compose.yaml`
2. Add a file `participants.txt` and insert a new line for each participant with the AUID and full name of each member separated by a whitespace
3. Add `participants.txt` to the folder where your Docker files are located
4. Archive and compress the folder using one the following formats: `zip`, or `gzip/tar`. All other formats (`rar`, `7z`, etc.) will result in a request for resubmission
5. The filename should be named `<AUID_PARTICIPANT_1>-<AUID_PARTICIPANT_2>-<AUID_PARTICIPANT_3>.<ARCHIVE_COMPRESS_FORMAT>` _Example: Alice Alison with AUID `au01248` and Bob Bobson with AUID `au84210` creates a compressed archive named `au01248-au84210.tar.gz` and uploads that to Brightspace_

A `participants.txt` example:
```
au01248 Alice Alison
au84210 Bob Bobson
```

[^1]: [Docker Hub Quickstart | Docker Documentation](https://docs.docker.com/docker-hub/)
[^2]: [Docker images for ASP.NET Core | Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-6.0)
[^3]: [Environment variables in Compose | Docker Documentation](https://docs.docker.com/compose/environment-variables/)
[^4]: [Networking in Compose | Docker Documentation](https://docs.docker.com/compose/networking/)