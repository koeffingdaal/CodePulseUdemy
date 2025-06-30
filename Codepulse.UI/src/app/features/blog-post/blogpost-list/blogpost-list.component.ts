import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {

  // Observable to hold all blog posts
  blogPost$?: Observable<BlogPost[]>;

  // Array to hold blog posts for the current page
  pagedBlogPosts: any[] = [];

  // Pagination settings
  currentPage = 1;
  pageSize = 5; // Number of posts per page
  totalPages = 0; // Total number of pages calculated dynamically

  // Injecting the BlogPostService to fetch blog posts
  constructor(private service: BlogPostService) { }

  // Lifecycle hook that runs once the component is initialized
  ngOnInit(): void {
    this.loadBlogPost();
  }

  private loadBlogPost() : void {
    // Fetch all blog posts
    this.blogPost$ = this.service.getAllBlogPost();

    // Subscribe to the observable to get post data
    this.blogPost$.subscribe(posts => {
      this.totalPages = Math.ceil(posts.length / this.pageSize); // Calculate total pages
      this.setPage(1, posts); // Load first page
    });
  }

  // Sets the current page and slices the post list for display
  setPage(page: number, posts: any[]) {
    this.currentPage = page;
    const startIndex = (page - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.pagedBlogPosts = posts.slice(startIndex, endIndex); // Set paged data
  }

  // Returns a Bootstrap class based on the category name for styling badges
  getBadgeClass(categoryName: string): string {
    switch (categoryName.toLowerCase()) {
      case 'java':
        return 'bg-warning text-dark'; // Yellow badge
      case 'java script':
      case 'javascript':
        return 'bg-info text-dark'; // Light blue badge
      case 'python':
        return 'bg-success'; // Green badge
      case 'c#':
        return 'bg-primary'; // Blue badge
      default:
        return 'bg-secondary'; // Default gray badge
    }
  }

}
