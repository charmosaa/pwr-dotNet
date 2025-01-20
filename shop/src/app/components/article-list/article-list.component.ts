import { Component, OnInit, inject, signal } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article.model';
import { ArticleFormComponent } from '../article-form/article-form.component';
import { ArticleItemComponent } from '../article-item/article-item.component';
import { ArticleDetailsComponent } from '../article-details/article-details.component';

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
    this.currentArticle = this.articleService.getArticle(id);
    this.isArticleModifyOpen = true;
  }

  onArticleModifyCancel(){
    this.isArticleModifyOpen = false;
  }

  onArticleModifyStart(){
    this.isArticleModifyOpen = true;
  }

  onArticleModifySave(modifyDate: {name: string, category: string, price: number}){
    console.log('Article modified:', modifyDate);
  }

}
