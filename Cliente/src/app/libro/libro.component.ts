import { Component, Input, EventEmitter, Output } from '@angular/core';
import { LibrosService } from '../services/libros.services';

@Component({
  selector: 'app-libro',
  templateUrl: './libro.component.html',
  styleUrl: './libro.component.css'
})
export class LibroComponent {

@Input() tituloLibro: string = '';

 
constructor(private service: LibrosService) { }
  


onClicked() {
  this.service.removeLibro(this.tituloLibro);
}

}
