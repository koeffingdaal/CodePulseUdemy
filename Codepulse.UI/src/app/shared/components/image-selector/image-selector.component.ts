import { Component, OnInit, ViewChild } from '@angular/core';
import { ImageService } from './image.service';
import { Observable } from 'rxjs';
import { BlogImage } from '../../models/blog-image.model';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent implements OnInit {

  private file?: File;

  fileName: string = '';
  title: string = '';

  image$?: Observable<BlogImage[]>

  @ViewChild('form', { static: false }) imageUploadForm?: NgForm;

  constructor(private imageService: ImageService) {

  }
  ngOnInit(): void {
    this.getImages();
  }

  private getImages() {
    this.image$ = this.imageService.getAllImages();
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
    if (this.file && this.fileName !== '' && this.title !== '') {
      // Image Service to upload the image
      this.imageService.uploadImage(this.file, this.fileName, this.title)
        .subscribe({
          next: (response) => {
            //console.log(response);
            this.imageUploadForm?.resetForm();
            this.getImages();
          }
        });
    }
  }

  selectImage(image: BlogImage): void{
    this.imageService.selectImage(image);
  }
}
