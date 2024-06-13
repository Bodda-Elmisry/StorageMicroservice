# StorageMicroservice
Microservice that can be used to store files from any extensions and act as storage hub for all other microservices.

- The function and nonfunctional requirement for this microservice :-
	Functional Requirements:
		1- File Upload: Accept files from various microservices.
		2- File Download: Provide a mechanism for retrieving files.
		3- File Deletion: Allow deletion of files.
		4- File Metadata Storage: Store metadata (e.g., file size, upload date, file type).
	Nonfunctional Requirements:
		1- Scalability: Handle a large number of files and concurrent access.
		2- Reliability: Ensure files are consistently stored and retrievable.
		3- Performance: Optimize for quick upload and retrieval times.
		4- Security: Ensure files are stored securely and access is controlled.
		5- Cost-effectiveness: Manage storage costs effectively, especially with large volumes.

- The high level and low-level design for the microservice :- 
	High-Level Design:
		1- API Gateway: Routes requests to the storage microservice.
		2- Storage Service: Handles file operations (upload, download, delete).
		3- Database: Stores metadata about files.
		4- StorageProviderFactory : Switch between the storage providers.
		5- Local Storage/Azure Storage: Physical storage locations for files.
	Low-Level Design:
		1- Controllers: Handle incoming HTTP requests.
		2- Services: Business logic for file operations.
		3- Repositories: Interact with the database for metadata operations.
		4- Storage Providers: Interface for different storage types (local and Azure).
		5- Factory : Switch between the storage providers.

- The type of the storage that will be used and why, and how the files will be saved and retrieved :-
	Storage Type:
		1- Local Storage: Useful for development and testing. Files are stored on the server’s filesystem.
		2- Azure Blob Storage: Scalable and cost-effective for production. Provides high availability, durability and security.
	File Management:
		1- Local : Save files to a specific directory on the server and retrive it from the same path.
		2- Saving Files On Azure Blob : Use azure blob storage to upload files on the server and retrive it.

- How the other microservice will communicate with the new designed one?
	Other microservices will communicate with storage microservice using http requests.
