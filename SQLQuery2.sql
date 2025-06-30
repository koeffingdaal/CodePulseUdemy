SELECT TOP (1000) [Id]
      ,[Name]
      ,[UrlHandle]
  FROM [CodePulse].[dbo].[Categories]


select * from Categories join BlogPosts on Categories.Id = BlogPosts.Id;

select * from Categories left join BlogPosts on Categories.Id = BlogPosts.Id;

select * from Categories right join BlogPosts on Categories.Id = BlogPosts.Id;


select * from BlogPosts full outer join Categories on Categories.Id = BlogPosts.Id;

select * from BlogPostCategory cross join Categories;

SELECT A.Id, A.Name, B.Name AS RelatedCategory
FROM Categories A
JOIN Categories B 
ON A.Id != B.Id;



select * 
from Categories 
left join BlogPostCategory on Categories.Id = BlogPostCategory.CategoriesId
left join BlogPosts on BlogPostCategory.BlogPostId = BlogPosts.Id;

select * 
from Categories 
right join BlogPostCategory on Categories.Id = BlogPostCategory.CategoriesId
right join BlogPosts on BlogPostCategory.BlogPostId = BlogPosts.Id;




select * 
from Categories 
right join BlogPostCategory on Categories.Id = BlogPostCategory.CategoriesId
left join BlogPosts on BlogPostCategory.BlogPostId = BlogPosts.Id;