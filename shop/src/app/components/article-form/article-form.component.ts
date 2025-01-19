import { Component, EventEmitter, Output } from '@angular/core';
import { Article } from '../../models/article.model';
import { Category, CATEGORIES_LIST } from '../../models/category.model';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-article-form',
  templateUrl: './article-form.component.html',
  styleUrls: ['./article-form.component.css'],
  imports: [FormsModule, NgFor],
})
export class ArticleFormComponent {
  @Output() addArticleEvent = new EventEmitter<Article>();

  newArticle: Partial<Article> = {};
  categories: Category[] = CATEGORIES_LIST;  

  
  ngOnInit(): void {
    // Log categories to see if they are populated
    console.log('Categories:', this.categories);
  }

  addArticle(): void {
    if (this.newArticle.name && this.newArticle.category && this.newArticle.price) {
      const article: Article = { ...this.newArticle, id: Date.now() } as Article;
      this.addArticleEvent.emit(article);
      console.log('Article added:', article);  
      this.newArticle = {}; 
    }
    else {
      console.log('Form is incomplete:', this.newArticle); 
    }
  }
  
}
