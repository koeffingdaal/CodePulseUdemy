export interface AddBlogPost {
  Title: string;
  ShortDescription: string;
  Content: string;
  FeaturedImageUrl: string;
  UrlHandle: string;  // Changed from urHandle to urlHandle
  Author: string;
  PublishedDate: Date;
  IsVisible: boolean;
  Categories: string[];
}
