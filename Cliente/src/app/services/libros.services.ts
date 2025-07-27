import { NgForm } from "@angular/forms";

export class LibrosService {
  private libros: string[] = ['Libro 1', 'Libro 2', 'Libro 3'];

  getLibros(): string[] {
    return [...this.libros];
  }

guardarLibro(libro: string) {
      this.libros.push(libro);
  }

  removeLibro(libro: string): void {
    this.libros = this.libros.filter(l => l !== libro);
    console.log('Libro eliminado:', libro);
  }
}