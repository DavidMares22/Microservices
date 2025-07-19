import { Component } from '@angular/core';

@Component({
  selector: 'app-libros',
  templateUrl: './libros.component.html',
  styleUrl: './libros.component.css'
})
export class LibrosComponent {
 
  libros: string[] = ['Libro 1', 'Libro 2', 'Libro 3'];

  eliminarLibro(libro: string) {
    this.libros = this.libros.filter(l => l !== libro);
    console.log('Libro eliminado:', libro);
  }

}
