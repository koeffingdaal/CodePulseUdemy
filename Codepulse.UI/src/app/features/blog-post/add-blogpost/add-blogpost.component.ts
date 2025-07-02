import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit, OnDestroy {

  model: AddBlogPost;

  isImageSelectorVisible: boolean = false;

  categories$?: Observable<Category[]>

  imageSelectorSubscription?: Subscription;


  constructor(private blogPostService: BlogPostService, private categoryService: CategoryService, private router: Router, private imageService: ImageService) {
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
    this.loadImage();
  }

  private loadImage(): void {
    this.imageSelectorSubscription = this.imageService.onSelectImage()
      .subscribe({
        next: (selectedImage) => {
          this.model.FeaturedImageUrl = selectedImage.url;
          this.closeImageSelector();
        }
      });
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

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }


}
