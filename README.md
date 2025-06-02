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

Image Upload UI Implementation
📁 File Structure (Frontend)


src/
├── components/
│   ├── ImageUpload/
│   │   ├── ImageUpload.tsx      # Main component
│   │   ├── ImageUpload.css      # Styles
│   │   └── types.ts             # Type definitions
├── pages/
│   └── BlogEditorPage.tsx       # Example usage
└── services/
    └── apiClient.ts             # API service



1. API Service (services/apiClient.ts)

import axios from 'axios';

const API_BASE_URL = 'https://your-api-url.com/api';

export const uploadImage = async (
  file: File,
  fileName: string,
  title: string
) => {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('fileName', fileName);
  formData.append('title', title);

  const response = await axios.post(`${API_BASE_URL}/Image`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response.data;
};

2. Image Upload Component (components/ImageUpload/ImageUpload.tsx)

import React, { useState } from 'react';
import { Button, Box, TextField, CircularProgress, Typography } from '@mui/material';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import { uploadImage } from '../../services/apiClient';
import './ImageUpload.css';

interface ImageUploadProps {
  onUploadSuccess: (imageData: BlogImageDto) => void;
}

const ImageUpload: React.FC<ImageUploadProps> = ({ onUploadSuccess }) => {
  const [file, setFile] = useState<File | null>(null);
  const [fileName, setFileName] = useState('');
  const [title, setTitle] = useState('');
  const [isUploading, setIsUploading] = useState(false);
  const [error, setError] = useState('');

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setFile(e.target.files[0]);
      setFileName(e.target.files[0].name.split('.')[0]);
    }
  };

  const handleSubmit = async () => {
    if (!file || !fileName || !title) {
      setError('All fields are required');
      return;
    }

    setIsUploading(true);
    setError('');

    try {
      const imageData = await uploadImage(file, fileName, title);
      onUploadSuccess(imageData);
      resetForm();
    } catch (err) {
      setError('Failed to upload image. Please try again.');
    } finally {
      setIsUploading(false);
    }
  };

  const resetForm = () => {
    setFile(null);
    setFileName('');
    setTitle('');
  };

  return (
    <Box className="image-upload-container">
      <Typography variant="h6" gutterBottom>
        Upload Blog Image
      </Typography>

      <Box className="upload-area">
        <input
          accept="image/jpeg,image/png"
          id="contained-button-file"
          type="file"
          onChange={handleFileChange}
          style={{ display: 'none' }}
        />
        <label htmlFor="contained-button-file">
          <Button
            variant="contained"
            component="span"
            startIcon={<CloudUploadIcon />}
          >
            Select Image
          </Button>
        </label>
        {file && (
          <Typography variant="body2" sx={{ mt: 1 }}>
            Selected: {file.name}
          </Typography>
        )}
      </Box>

      <TextField
        label="File Name"
        value={fileName}
        onChange={(e) => setFileName(e.target.value)}
        fullWidth
        margin="normal"
      />

      <TextField
        label="Image Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        fullWidth
        margin="normal"
      />

      {error && (
        <Typography color="error" sx={{ mt: 1 }}>
          {error}
        </Typography>
      )}

      <Button
        variant="contained"
        color="primary"
        onClick={handleSubmit}
        disabled={isUploading || !file}
        sx={{ mt: 2 }}
      >
        {isUploading ? (
          <>
            <CircularProgress size={24} color="inherit" />
            <span style={{ marginLeft: 8 }}>Uploading...</span>
          </>
        ) : (
          'Upload Image'
        )}
      </Button>
    </Box>
  );
};

export default ImageUpload;



3. Styles (components/ImageUpload/ImageUpload.css)
.image-upload-container {
  padding: 24px;
  border: 1px dashed #ccc;
  border-radius: 4px;
  background-color: #fafafa;
}

.upload-area {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  margin-bottom: 16px;
}

.preview-image {
  max-width: 100%;
  max-height: 200px;
  margin-top: 16px;
  border-radius: 4px;
}

4. Type Definitions (components/ImageUpload/types.ts)

export interface BlogImageDto {
  id: string;
  fileName: string;
  title: string;
  fileExtension: string;
  url: string;
  dateCreated: string;
}

5. Example Usage (pages/BlogEditorPage.tsx)

import React from 'react';
import ImageUpload from '../components/ImageUpload/ImageUpload';
import { BlogImageDto } from '../components/ImageUpload/types';

const BlogEditorPage: React.FC = () => {
  const handleUploadSuccess = (imageData: BlogImageDto) => {
    console.log('Image uploaded successfully:', imageData);
    // You can use the imageData.url in your blog content
  };

  return (
    <div style={{ maxWidth: '600px', margin: '0 auto' }}>
      <h1>Blog Editor</h1>
      <ImageUpload onUploadSuccess={handleUploadSuccess} />
    </div>
  );
};

export default BlogEditorPage;

npm install @mui/material @emotion/react @emotion/styled @mui/icons-material axios







