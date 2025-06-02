# CodePulse API - Blog Management System

![CodePulse Logo](https://via.placeholder.com/150x50?text=CodePulse)

A comprehensive blog management API with image upload capabilities, built with ASP.NET Core.

## 📌 Repository Structure
CodePulseUdemy/
├── CodePluse.API/
│ ├── Controllers/ # API controllers
│ ├── Data/ # Database context and migrations
│ ├── Filters/ # Swagger and custom filters
│ ├── Models/ # Domain models and DTOs
│ ├── Repository/ # Data access layer
│ ├── Program.cs # Application entry point
│ └── appsettings.json # Configuration
├── Images/ # Uploaded images storage
└── README.md # This file


## 🖼️ Image Upload Feature

### Key Components

1. **ImageController** (`Controllers/ImageController.cs`)
   - Handles HTTP POST requests for image uploads
   - Validates file type (JPG/JPEG/PNG) and size (<10MB)
   - Returns image metadata with generated URL

2. **ImageRepository** (`Repository/ImageRepository.cs`)
   - Saves files to `Images/` directory
   - Stores metadata in database
   - Generates public URLs

3. **BlogImage Model** (`Models/Domain/BlogImage.cs`)
   - Contains image metadata (ID, filename, title, extension, URL, creation date)

### 🚀 How to Upload Images

**Endpoint:**


**Request Format:**
```json
Content-Type: multipart/form-data

{
  "file": "[binary image data]",
  "fileName": "my-image",
  "title": "Summer Vacation"
}


{
  "id": "a1b2c3d4-e5f6-7890",
  "fileName": "my-image",
  "title": "Summer Vacation",
  "fileExtension": ".jpg",
  "url": "https://yourapi.com/Images/my-image.jpg",
  "dateCreated": "2023-08-15T10:30:00Z"
}

{
  "id": "a1b2c3d4-e5f6-7890",
  "fileName": "my-image",
  "title": "Summer Vacation",
  "fileExtension": ".jpg",
  "url": "https://yourapi.com/Images/my-image.jpg",
  "dateCreated": "2023-08-15T10:30:00Z"
}

{
  "ConnectionStrings": {
    "Con": "Server=.;Database=CodePulseDB;Trusted_Connection=True;"
  }
}

dotnet restore
dotnet run


https://localhost:7204/swagger


This README includes:

1. Clear repository structure visualization
2. Detailed documentation of the image upload feature
3. API endpoint specifications
4. Setup instructions
5. Troubleshooting guide
6. License information

You can customize the:
- Database configuration details
- Deployment URLs
- License information
- Course reference links

Would you like me to add any specific additional sections or modify any part of this README?


