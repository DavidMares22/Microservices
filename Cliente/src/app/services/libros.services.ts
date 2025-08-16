import { NgForm } from "@angular/forms";
import { Subject } from "rxjs";


export class LibrosService {
  private libros: string[] = ['Libro 1', 'Libro 2', 'Libro 3'];
  librosSubject = new Subject<void>();



  getLibros(): string[] {
    return [...this.libros];
  }

guardarLibro(libro: string) {
    this.libros.push(libro);
    this.librosSubject.next();
  }

  removeLibro(libro: string): void {
    this.libros = this.libros.filter(l => l !== libro);
    console.log('Libro eliminado:', libro);
    this.librosSubject.next();
  }
}