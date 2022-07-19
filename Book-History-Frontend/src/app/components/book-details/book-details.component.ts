import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IBook } from 'src/app/interface/IBook';
import { IAuthor } from 'src/app/interface/IAuthor';
import { BookService } from 'src/app/services/book.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { AuthorService } from 'src/app/services/author.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  currentBook = {} as IBook;
  currentAuthors: IAuthor[] = [];

  message = '';


  dropdownList: any = [];
  selectedItems: any = [];
  dropdownSettings: IDropdownSettings = {};

  constructor(
    private bookService: BookService,
    private authorService: AuthorService,
    private route: ActivatedRoute,
    private router: Router) { }
  ngOnInit(): void {
    this.message = '';

    this.retrieveauthors();

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
    };
  }

  retrieveauthors(): void {
    let tmp: { item_id: string; item_text: string }[] = [];

    this.authorService.getAll().subscribe(
      (authors) => {
        authors.forEach(
          (author: { authorId: string; authorName: string }) => {
            tmp.push({ item_id: author.authorId, item_text: author.authorName });
          }
        );

        this.dropdownList = tmp;

        this.getBook(this.route.snapshot.paramMap.get('detail'));

        console.log(this.dropdownList);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getBook(id: any): void {
    let tmp: { item_id: string; item_text: string }[] = [];

    this.bookService.get(id)
      .subscribe(
        data => {
          this.currentBook = data;

          data.authors.forEach(
            (book: any) => {
              tmp.push({ item_id: book.authorId, item_text: book.authorName });
            }
          );

          this.selectedItems = tmp;
          this.initAuthor();
        },
        error => {
          console.log(error);
        });
  }

  updateBook(): void {

    this.currentBook.authors = this.currentAuthors;

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

  onItemSelect(item: any) {
    this.initAuthor();
    console.log(item);
  }
  onSelectAll(items: any) {
    this.initAuthor();
    console.log(items);
  }
  onItemDeSelect(items: any) {
    this.initAuthor();
    console.log(items);
  }



  initAuthor() {
    let tmpAuthors: IAuthor[] = [];

    this.selectedItems.forEach(
      (author: any) => {
        tmpAuthors.push({ authorId: author.item_id, authorName: author.item_text })
      }
    );

    this.currentAuthors = tmpAuthors;

  }
}
