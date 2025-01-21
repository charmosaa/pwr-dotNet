import { Component, OnInit, inject, signal } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article.model';
import { ArticleFormComponent } from '../article-form/article-form.component';
import { ArticleItemComponent } from '../article-item/article-item.component';
import { ArticleDetailsComponent } from '../article-details/article-details.component';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css'],
  imports: [ArticleFormComponent, ArticleItemComponent,ArticleDetailsComponent],
})
export class ArticleListComponent implements OnInit {
  private articleService = inject(ArticleService);
  articles = signal<Article[]>([]);
  isArticleModifyOpen = false;
  currentArticle?: Article;

  constructor() {}

  ngOnInit(): void {
    this.articles.set(this.articleService.getArticles());
  }

  handleAddArticle(article: Article): void {
    this.articleService.addArticle(article);
    this.articles.set(this.articleService.getArticles());
  }

  onSelect(id: number){
    this.isArticleModifyOpen = false;
    this.currentArticle = this.articleService.getArticle(id);
    this.isArticleModifyOpen = true;
  }

  onArticleModifyCancel(){
    this.isArticleModifyOpen = false;
  }

  onArticleModifyStart(){
    this.isArticleModifyOpen = true;
  }

  onArticleModifyRemove(){
    this.articleService.removeArticle(this.currentArticle!.id);
    this.articles.set(this.articleService.getArticles());
    this.currentArticle = undefined;
    this.isArticleModifyOpen = false;
  }

  onArticleModifySave(modifyDate: {name: string, category: Category, price: number}){
    this.currentArticle!.name = modifyDate.name;
    this.currentArticle!.category = modifyDate.category;
    this.currentArticle!.price = modifyDate.price;
    this.isArticleModifyOpen = false;
  }

}
