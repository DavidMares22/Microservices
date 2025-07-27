import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LibrosService } from '../services/libros.services';

@Component({
  selector: 'app-libros',
  templateUrl: './libros.component.html',
  styleUrl: './libros.component.css'
})
export class LibrosComponent {
  

  constructor(private bookService : LibrosService) {}

  libros: string[] = this.bookService.getLibros();

guardarLibro(form: NgForm) {
    if (form.valid) {
      this.bookService.guardarLibro(form.value['nombre del libro']);
      form.reset();
      this.libros = this.bookService.getLibros();
    }
  }

  eliminarLibro(libro: string) {
    this.bookService.removeLibro(libro);
    this.libros = this.bookService.getLibros();
    console.log('Lista actualizada:', this.libros);
  }
}
