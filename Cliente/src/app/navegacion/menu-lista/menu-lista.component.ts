import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
 

@Component({
  selector: 'app-menu-lista',
  templateUrl: './menu-lista.component.html',
  styleUrls: ['./menu-lista.component.css']
})
export class MenuListaComponent implements OnInit, OnDestroy {
  @Output() menuToggle = new EventEmitter<void>();
 


  constructor( ) { }

  ngOnInit(): void {
     
  }

  onCerrarMenu(){
    this.menuToggle.emit();
  }

  terminarSesionMenu(){
    this.onCerrarMenu();
  }

  ngOnDestroy(){
  }

}
