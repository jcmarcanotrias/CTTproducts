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


# Recommendations for Production Projects and Environments

## 1. **Disable Debugging Tools (Swagger)**
   Disable Swagger and other debugging tools in production environments to avoid exposing sensitive API details.

## 2. **Use Environment Variables for Configuration**
   Store sensitive data and environment-specific configurations in environment variables rather than in `appsettings.json`.

## 3. **Proper Error Handling**
   Ensure your application handles errors gracefully, logs them appropriately, and avoids exposing stack traces to users.

## 4. **Performance Optimization**
   Use caching mechanisms, optimize database queries with proper indexing, and make use of async programming to improve scalability.

## 5. **Security Best Practices**
   - Enforce HTTPS in production.
   - Use authentication and authorization for APIs.
   - Ensure all user input is validated to prevent attacks.

## 6. **Logging and Monitoring**
   Implement centralized logging and health checks for real-time monitoring of application status and performance.

## 7. **CI/CD Pipeline**
   Automate your build and deployment processes using CI/CD pipelines to ensure consistency in deployments.

## 8. **Backup Strategy**
   Regularly back up your data and ensure that recovery is quick and reliable in case of failure.

## 9. **Secrets Management**
   Use a dedicated secrets management tool for handling sensitive data like API keys or database credentials.

## 10. **Scalability**
   Ensure that your application can scale horizontally and consider using container orchestration tools like Kubernetes for managing multiple instances.

## Final Thoughts
   By following these best practices, you can ensure that your application is secure, reliable, and ready for production.

