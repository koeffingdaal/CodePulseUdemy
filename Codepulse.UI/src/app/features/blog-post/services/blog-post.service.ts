import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable, retry, tap } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { environment } from 'src/environments/environment';
import { UpdateBlogPost } from '../models/update-blog-post.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) {}

  createBlogPost(blogPostData: AddBlogPost): Observable<BlogPost> {
    return this.http.post<BlogPost>(`${environment.apiBaseUrl}/api/BlogPosts`, blogPostData);
  }

  getAllBlogPost() : Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(`${environment.apiBaseUrl}/api/BlogPosts`);
  }

  getBlogPostById(id: string) : Observable<BlogPost> {
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/BlogPosts/${id}`);
  }

  getBlogPostByUrlHandle(urlHandle: string) : Observable<BlogPost> {
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/BlogPosts/${urlHandle}`);
  }

  updateBlogPost(id: string, updatedBlogPost: UpdateBlogPost) : Observable<BlogPost> {
    return this.http.put<BlogPost>(`${environment.apiBaseUrl}/api/BlogPosts/${id}`, updatedBlogPost);
  }

  deleteBlogPost(id: string) : Observable<BlogPost> {
    return this.http.delete<BlogPost>(`${environment.apiBaseUrl}/api/BlogPosts/${id}`);
  }
}
