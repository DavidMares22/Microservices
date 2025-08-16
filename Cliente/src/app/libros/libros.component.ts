import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LibrosService } from '../services/libros.services';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-libros',
  templateUrl: './libros.component.html',
  styleUrl: './libros.component.css'
})
export class LibrosComponent {


  constructor(private bookService: LibrosService) { }

  libros: string[] = [];
  private libroSubscription!: Subscription;

  ngOnInit() {

    this.libros = this.bookService.getLibros();
    this.libroSubscription  = this.bookService.librosSubject.subscribe(() => {
      this.libros = this.bookService.getLibros();
      console.log('Lista actualizada:', this.libros);
    });
  }
  ngOnDestroy() {
    if (this.libroSubscription)
    {
      console.log('Desuscribiendo de librosSubject');
      this.libroSubscription.unsubscribe();
    }
  }

  guardarLibro(form: NgForm) {
    if (form.valid) {
      this.bookService.guardarLibro(form.value['nombre del libro']);
      form.reset();
      this.libros = this.bookService.getLibros();

    }
  }

  
}
