import { Component, inject, signal } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article.model';
import { ArticleFormComponent } from '../article-form/article-form.component';
import { NgFor } from '@angular/common';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css'],
  imports: [NgFor, ArticleFormComponent],
})
export class ArticlesComponent {
  private articleService = inject(ArticleService);
  articles = signal<Article[]>([]);
  categories = signal<Category[]>([]);

  constructor() {}

  ngOnInit(): void {
    this.articles.set(this.articleService.getArticles());
  }

  handleAddArticle(article: Article): void {
    this.articleService.addArticle(article);
    this.articles.set(this.articleService.getArticles());
  }

  deleteArticle(id: number): void {
    this.articleService.removeArticle(id);
    this.articles.set(this.articleService.getArticles());
  }
}
