
import { temporaryAllocator } from '@angular/compiler/src/render3/view/util';
import { Component, OnInit, TemplateRef } from '@angular/core';

import { Evento } from '../../models/Evento';
import { EventoService } from '../../services/evento.service';


import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
 ngOnInit(): void {

 }
}
