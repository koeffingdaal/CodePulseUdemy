import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit  {

  model: AddBlogPost;

  isImageSelectorVisible: boolean = false;

  categories$? : Observable<Category[]>


  constructor(private blogPostService: BlogPostService, private categoryService: CategoryService, private router: Router) {
    this.model = {
      Title: '',
      ShortDescription: '',
      UrlHandle: '',
      Content: '',
      FeaturedImageUrl: '',
      Author: '',
      IsVisible: true,
      PublishedDate: new Date(),
      Categories: []
    }
  }
  ngOnInit(): void {
    this.loadAllCategories();
  }

  private loadAllCategories(): void {
    this.categories$ = this.categoryService.getAllCategories();
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

  clearContent(): void {
    this.model.Content = ''

  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector() : void {
    this.isImageSelectorVisible = false;
  }

}
