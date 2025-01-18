import { Injectable } from '@angular/core';
import { CATEGORIES_LIST, Category } from '../models/category.model';


export interface Article {
  id: number;
  name: string;
  category: Category;
  price: number;
  imagePath?: string; 
}

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  private categories: Category[] = CATEGORIES_LIST;
  private articles: Article[] = [
    { id: 1, name: 'Sunflower', category: 'Flowers', price: 12.56 },
    { id: 2, name: 'Lemon', category: 'Food', price: 2.09 },
    { id: 3, name: 'Orange', category: 'Food', price: 2.19 },
  ];

  constructor() {}

  getCategories(): Category[] {
    return this.categories;
  }

  getArticles(): Article[] {
    return this.articles;
  }

  addArticle(article: Article): void {
    this.articles.push(article);
  }

  removeArticle(id: number): void {
    this.articles = this.articles.filter(article => article.id !== id);
  }
}
