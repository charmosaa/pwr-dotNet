import { Category } from './category.model';

export interface Article {
  id: number;
  name: string;
  category: Category;
  price: number;
  imagePath?: string; 
}