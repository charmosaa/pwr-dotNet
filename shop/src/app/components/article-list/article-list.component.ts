import { Component, inject, signal } from '@angular/core';
import { Article, ArticleService } from '../../services/article.service';
import { Category } from '../../models/category.model';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-article-list',
  imports: [NgFor],
  templateUrl: './article-list.component.html',
  styleUrl: './article-list.component.css'
})


export class ArticlesComponent {
  private articleService = inject(ArticleService);
  articles = signal<Article[]>([]); // Reactive signal dla listy artykułów
  categories = signal<Category[]>([]); // Reactive signal dla kategorii
  newArticle: Partial<Article> = {};

  constructor() {}

  ngOnInit(): void {
    this.articles.set(this.articleService.getArticles());
    this.categories.set(this.articleService.getCategories());
  }

  addArticle(): void {
    if (this.newArticle.name && this.newArticle.category && this.newArticle.price) {
      const id = Math.max(0, ...this.articles().map((a) => a.id)) + 1;
      const article: Article = { ...this.newArticle, id } as Article;

      this.articleService.addArticle(article);

      // Aktualizacja sygnału
      this.articles.set(this.articleService.getArticles());
      this.newArticle = {};
    }
  }

  removeArticle(id: number): void {
    this.articleService.removeArticle(id);

    // Aktualizacja sygnału
    this.articles.set(this.articleService.getArticles());
  }
}