import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-libro',
  templateUrl: './libro.component.html',
  styleUrl: './libro.component.css'
})
export class LibroComponent {

@Input() tituloLibro: string = '';
@Output() libroClicked: EventEmitter<void> = new EventEmitter<void>();

onClicked() {
  console.log('Libro clicked:', this.tituloLibro);
  this.libroClicked.emit();
}

}
