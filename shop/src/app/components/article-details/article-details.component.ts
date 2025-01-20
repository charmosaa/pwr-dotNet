import { Component, input, output, signal, OnInit, computed} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Article } from '../../models/article.model';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-article-details',
  imports: [FormsModule],
  templateUrl: './article-details.component.html',
  styleUrl: './article-details.component.css'
})
export class ArticleDetailsComponent implements OnInit {
  article=input.required<Article>();
  cancel=output<void>();
  save=output<{name:string, category:Category, price:number}>();

  enteredName = signal("");
  enteredCategory: Category = "Other";
  enteredPrice = signal(0);

  ngOnInit(): void {
    this.enteredName.set(this.article().name);
    this.enteredCategory = this.article().category;
    this.enteredPrice.set(this.article().price);
  }

  onCancel(): void {
    this.cancel.emit();
  }

  onSubmit(): void {
    this.save.emit({name: this.enteredName(), category: this.enteredCategory, price: this.enteredPrice()});
  }

  imagePath = computed(() => {
    if(this.article().imagePath)
      return 'assets/articles/' + this.article().imagePath;
    return 'assets/articles/default.png';
  });
}
