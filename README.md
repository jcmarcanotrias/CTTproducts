## Instructions for Running Docker Locally and Connecting Application to MongoDB

Below are the instructions to run the Docker container locally and connect the application from one container to another container running MongoDB:

1. **Download the application image** with the following command from a console window:

   ```bash
   docker pull jcmarcanotrias/cttproducts:latest
   ```

2. **Run the container** with the following command:

   ```bash
   docker run -d -p 8080:8080 --name cttproducts-container jcmarcanotrias/cttproducts:latest
   ```

   - The first `8080` is the local port on the host. If it's in use, it should be changed to another port.
   - The second `8080` is the internal port of Docker.

3. **Download the latest MongoDB image** with the following command:

   ```bash
   docker pull mongo:latest
   ```

4. **Create a container using the MongoDB image** with the following command:

   ```bash
   docker run -d --name mongodb-local -p 27017:27017 mongo
   ```

5. **Create a Docker network** so that the containers can communicate:

   ```bash
   docker network create ctt-network
   ```

6. **Connect both containers to the network**:

   ```bash
   docker network connect ctt-network cttproducts-container
   docker network connect ctt-network mongodb-local
   ```

7. **Edit the `appsettings.json` file inside the `cttproducts-container` container**. The easiest way is through Docker Desktop. View the container's details, go to the "Files" tab, navigate to the `app` folder, and inside you will find the `appsettings.json` file. Right-click the file and select "Edit". Change the connection string from `localhost` to:

   ```json
   "ConnectionString": "mongodb://mongodb-local:27017"
   ```

   Then, restart the container.

8. **Test the application using Swagger** (it was enabled in release mode to facilitate testing). In your browser, go to:

   ```
   http://localhost:8080/swagger/index.html
   ```
```
