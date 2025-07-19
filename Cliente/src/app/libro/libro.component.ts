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
  this.libroClicked.emit();
  console.log('Libro clicked:', this.tituloLibro);
}

}
