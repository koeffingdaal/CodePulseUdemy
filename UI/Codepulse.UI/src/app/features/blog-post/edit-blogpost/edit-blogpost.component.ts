import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  routeSubscription?: Subscription;
  categories$?: Observable<Category[]>;
  selectedCategories: string[] = [];

  model: BlogPost = {
    id: '',
    title: '',
    shortDescription: '',
    content: '',
    author: '',
    isVisible: true,
    categories: [],
    featureImageUrl: '',
    urlHandle: '',
    publishedDate: new Date()
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService
  ) {}

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
          this.blogPostService.getBlogPostById(this.id).subscribe({
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

  get publishedDateInput(): string {
    if (!this.model.publishedDate) return '';
    const date = new Date(this.model.publishedDate);
    if (isNaN(date.getTime())) return '';
    const offset = date.getTimezoneOffset();
    date.setMinutes(date.getMinutes() - offset);
    return date.toISOString().split('T')[0];
  }

  set publishedDateInput(value: string) {
    if (value) {
      const date = new Date(value);
      const offset = date.getTimezoneOffset();
      date.setMinutes(date.getMinutes() + offset);
      this.model.publishedDate = date;
    } else {
      this.model.publishedDate = new Date();
    }
  }

  onFormSubmit(): void {

  }

  private handleError(message: string): void {
    alert(message);
    this.router.navigate(['/admin/blogposts']);
  }

  private showSuccess(message: string): void {
    alert(message);
  }

  clearContent(): void {
    this.model = {
      ...this.model,
      title: '',
      shortDescription: '',
      content: '',
      author: '',
      categories: [],
      featureImageUrl: '',
      urlHandle: '',
      publishedDate: new Date()
    };
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }
}
