import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IBook } from 'src/app/interface/IBook';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  currentBook = {} as IBook;
  message = '';
  constructor(
    private bookService: BookService,
    private route: ActivatedRoute,
    private router: Router) { }
  ngOnInit(): void {
    this.message = '';
    this.getBook(this.route.snapshot.paramMap.get('id'));
  }
  getBook(id: any): void {
    this.bookService.get(id)
      .subscribe(
        data => {
          this.currentBook = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }
  updatepublishDate(status: any): void {
    const data = {
      title: this.currentBook.title,
      description: this.currentBook.description,
      publishDate: status,
      authors: [{"authorId": 0,
      "authorName": "string",
    "books": []}]
    };
    this.bookService.update(this.currentBook.id, data)
      .subscribe(
        response => {
          this.currentBook.publishDate = status;
          console.log(response);
        },
        error => {
          console.log(error);
        });
  }
  updateBook(): void {
    this.currentBook.authors = [{"authorId": 0,"authorName": "", "books":[]}];

    this.bookService.update(this.currentBook.id, this.currentBook)
      .subscribe(
        response => {
          console.log(response);
          this.message = 'The book was updated successfully!';
        },
        error => {
          console.log(error);
        });
  }
  deleteBook(): void {
    this.bookService.delete(this.currentBook.id)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/book']);
        },
        error => {
          console.log(error);
        });
  }
}