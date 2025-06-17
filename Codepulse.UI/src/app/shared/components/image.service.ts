import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogImage } from '../models/blog-image.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) { }

 uploadImage(file: File, fileName: string, title: string): Observable<BlogImage> {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('fileName', fileName);
  formData.append('title', title);

  // Log what's being sent to the server
  console.group('📤 Sending to Server');
  console.log('File:', file.name, `(${file.size} bytes)`);
  console.log('fileName:', fileName);
  console.log('title:', title);
  console.groupEnd();

  return this.http.post<BlogImage>(`${environment.apiBaseUrl}/api/Images`, formData);
}
}
