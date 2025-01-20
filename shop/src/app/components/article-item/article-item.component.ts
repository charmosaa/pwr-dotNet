import { Component, input, Input, output } from '@angular/core';
import { Article } from '../../models/article.model';

@Component({
  selector: 'app-article-item',
  templateUrl: './article-item.component.html',
  styleUrls: ['./article-item.component.css'],
})
export class ArticleItemComponent {
  article = input.required<Article>();
  selectedId = output<number>();

  onSelect(): void {
    this.selectedId.emit(this.article().id);
  }

  get imagePath(){
    if(this.article().imagePath)
      return 'assets/articles/' + this.article().imagePath;
    return 'assets/articles/default.png';

  }
}
