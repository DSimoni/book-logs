import { Component, OnInit } from '@angular/core';
import { IBook } from 'src/app/interface/IBook';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})
export class BooksListComponent implements OnInit {


  books: any;
  currentBook: IBook | undefined;
  currentIndex = -1;
  title = '';

  


  constructor(private bookService: BookService) { }
  ngOnInit(): void {
    this.retrievebooks();
  }
  retrievebooks(): void { 
    this.bookService.getAll()
      .subscribe(
        data => { 
          this.books = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }
  refreshList(): void {
    this.retrievebooks();
    this.currentBook = {} as IBook;
    this.currentIndex = -1;
  }

  orderBooks(): void {
    this.bookService.getOrderAll()
    .subscribe(
      data => { 
        this.books = data;
        console.log(data);
      },
      error => {
        console.log(error);
      });
  }

  setActivebook(book: any, index: number): void {
    this.currentBook = book;
    this.currentIndex = index;
  }

  searchTitle(): void {
    this.bookService.findByTitle(this.title)
      .subscribe(
        data => {
          this.books = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }
}

