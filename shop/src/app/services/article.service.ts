import { Injectable } from '@angular/core';
import { CATEGORIES_LIST, Category } from '../models/category.model';
import { Article } from '../models/article.model';



@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  private categories: Category[] = CATEGORIES_LIST;
  private articles: Article[] = [
    { id: 1, name: 'Sunflower', category: 'Flowers', price: 12.56 , imagePath: 'sunflower.jpg'},
    { id: 2, name: 'Lemon', category: 'Food', price: 2.09, imagePath: 'lemon.jpg' },
    { id: 3, name: 'Orange', category: 'Food', price: 2.19, imagePath: 'orange.jpg' },
    { id: 4, name: 'Rose', category: 'Flowers', price: 15.99, imagePath: 'rose.jpg' },
    { id: 5, name: 'Barbie', category: 'Toys', price: 6.99, imagePath: 'barbie.jpg' },
    { id: 6, name: 'Car', category: 'Toys', price: 1.99, imagePath: 'car.jpg' },
    { id: 7, name: 'Banana', category: 'Food', price: 2.99, imagePath: 'banan.jpg' },

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
