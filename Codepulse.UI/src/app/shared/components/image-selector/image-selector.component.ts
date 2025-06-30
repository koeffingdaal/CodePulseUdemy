import { Component } from '@angular/core';
import { ImageService } from './image.service';
@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent {

  private file?: File;

  fileName: string = '';
  title: string = '';

  constructor(private imageService: ImageService){

  }

  onFileUploadChange(event: Event): void {
  const element = event.currentTarget as HTMLInputElement;
  this.file = element.files?.[0];
}

//   uploadImage(): void {
//   if (this.file && this.fileName !== '' && this.title !== '') {
//     console.log('Uploading:', {
//       file: this.file,
//       name: this.file.name,
//       size: this.file.size,
//       type: this.file.type,
//       fileName: this.fileName,
//       title: this.title

//     });

//     this.imageService.uploadImage(this.file, this.fileName, this.title)
//       .subscribe({
//         next: (response) => console.log('✅ Success:', response),
//         error: (error) => {
//           console.error('❌ Error:', error);
//           if (error.error) {
//             console.log('Error details:', error.error);
//           }
//         }
//       });
//   }
// }

  uploadImage(): void {
    if (this.file && this.fileName !== '' && this.title !== ''){
      // Image Service to upload the image
      this.imageService.uploadImage(this.file, this.fileName, this.title)
      .subscribe({
        next: (response) => {
          console.log(response);
        }
      });
    }
  }
}
