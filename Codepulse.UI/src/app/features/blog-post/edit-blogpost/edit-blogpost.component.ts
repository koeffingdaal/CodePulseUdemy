import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateBlogPost } from '../models/update-blog-post.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  selectedCategories: string[] = [];
  isImageSelectorVisible: boolean = false;

  isSubmitting = false;

  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;

  categories$?: Observable<Category[]>;



  model?: BlogPost | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.loadCategories();
    this.loadBlogPostData();
  }

  private loadCategories(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

  private loadBlogPostData(): void {
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.getBlogPostSubscription = this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories = response.categories.map(x => x.id);
            },
            error: () => this.handleError('Blog post not found')
          });
        } else {
          this.handleError('Invalid blog post ID');
        }
      }
    });
  }

  goBack(): void {
    this.router.navigateByUrl('/admin/blogposts');
  }

  // get publishedDateInput(): string {
  //   if (!this.model.publishedDate) return '';
  //   const date = new Date(this.model.publishedDate);
  //   if (isNaN(date.getTime())) return '';
  //   const offset = date.getTimezoneOffset();
  //   date.setMinutes(date.getMinutes() - offset);
  //   return date.toISOString().split('T')[0];
  // }

  // set publishedDateInput(value: string) {
  //   if (value) {
  //     const date = new Date(value);
  //     const offset = date.getTimezoneOffset();
  //     date.setMinutes(date.getMinutes() + offset);
  //     this.model.publishedDate = date;
  //   } else {
  //     this.model.publishedDate = new Date();
  //   }
  // }



  onFormSubmit(): void {

    // Convert this model to request

    if (this.model && this.id) {
      var updateBlogPost: UpdateBlogPost = {
        title: this.model.title,
        shortDescription: this.model.shortDescription,
        content: this.model.content,
        featureImageUrl: this.model.featureImageUrl,
        urlHandle: this.model.urlHandle,
        author: this.model.author,
        publishedDate: this.model.publishedDate,
        isVisible: this.model.isVisible,
        categories: this.selectedCategories ?? []

      }

      this.blogPostService.updateBlogPost(this.id, updateBlogPost).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      })
    }

    this.router.navigateByUrl('/admin/blogposts');
  }

  onDelete() : void {

    if (this.id) {
      this.blogPostService.deleteBlogPost(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
    }
  }


  // get publishedDateInput(): string {
  //   if (!this.model?.publishedDate) return '';
  //   const date = new Date(this.model.publishedDate);
  //   return date.toISOString().split('T')[0];
  // }

  // set publishedDateInput(value: string) {
  //   if (this.model && value) {
  //     this.model.publishedDate = new Date(value);
  //   }
  // }

//   onFormSubmit(): void {
//   // Prevent multiple submissions
//   if (this.isSubmitting) return;

//   if (this.model && this.id) {
//     this.isSubmitting = true; // <-- Set loading state to true

//     const updateBlogPost: UpdateBlogPost = {
//       title: this.model.title,
//       shortDescription: this.model.shortDescription,
//       content: this.model.content,
//       featureImageUrl: this.model.featureImageUrl,
//       urlHandle: this.model.urlHandle,
//       author: this.model.author,
//       publishedDate: this.model.publishedDate,
//       isVisible: this.model.isVisible,
//       categories: this.selectedCategories ?? []
//     };

//     this.updateBlogPostSubscription = this.blogPostService.updateBlogPost(this.id, updateBlogPost)
//       .subscribe({
//         next: (response) => {
//           this.isSubmitting = false; // <-- Reset on success
//           this.showSuccess('Blog post updated successfully!');
//           this.router.navigateByUrl('/admin/blogposts');
//         },
//         error: (error) => {
//           this.isSubmitting = false; // <-- Reset on error
//           console.error('Error updating blog post:', error);
//           this.handleError('Failed to update blog post');
//         }
//       });
//   }
// }

  private handleError(message: string): void {
    alert(message);
    this.router.navigate(['/admin/blogposts']);
  }

  private showSuccess(message: string): void {
    alert(message);
  }

  clearContent(): void {
    if (this.model) {
      this.model.content = '';
    }
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector() : void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
  }
}
