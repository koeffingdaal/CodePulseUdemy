import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent {

  model: AddBlogPost;


  constructor(private blogPostService: BlogPostService, private router: Router) {
    this.model = {
      Title: '',
      ShortDescription: '',
      UrlHandle: '',
      Content: '',
      FeaturedImageUrl: '',
      Author: '',
      IsVisible: true,
      PublishedDate: new Date()
    }
  }

  onFormSubmit(): void {
    console.log('Submitting:', this.model); // For debugging
    this.blogPostService.createBlogPost(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        },
        error: (error) => {
          console.error('Error submitting blog post:', error);
        }
      });
  }

}
