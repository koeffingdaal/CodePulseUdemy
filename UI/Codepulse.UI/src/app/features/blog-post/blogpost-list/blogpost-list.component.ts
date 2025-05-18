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

  blogPost$?: Observable<BlogPost[]>
  pagedBlogPosts: any[] = [];

  // Pagination settings
  currentPage = 1;
  pageSize = 5;
  totalPages = 0;


  /**
   *
   */
  constructor(private service: BlogPostService) {


  }

  ngOnInit(): void {
    // Get all blogpost
    this.blogPost$ = this.service.getAllBlogPost();
    this.blogPost$.subscribe(posts => {
      this.totalPages = Math.ceil(posts.length / this.pageSize);
      this.setPage(1, posts);
    });

  }
  setPage(page: number, posts: any[]) {
    this.currentPage = page;
    const startIndex = (page - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.pagedBlogPosts = posts.slice(startIndex, endIndex);
  }

}
